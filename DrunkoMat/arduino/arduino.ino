#include <SPI.h>
#include <SD.h>

#define PUMPSPEED 30 // Speed of pump in ml/sec

const int chipSelect = 4;

// Recipe structure
typedef struct
{
  unsigned char Pin; // Pin of this recipe, you shuld set by numbers of arduino pins
  short Amount[6];   // Amount of each pump
} recipe;


unsigned char PumpPins[6];   // Pins of pumps
bool PumpState[6] = {false}; // State of each pump
recipe Resipes[6];           // All resipes


// This function check status of all pumps
// Input: Time when the coocking started, Reciept poiner
// Output: True if all pumps finished, False if not
bool ProcessFinish( unsigned long StartTime, recipe *Recipe )
{
  bool processtate = true;

  for (int i = 0; i < 6; i++)
    if (PumpState[i] == true)
      if (millis() - StartTime > (unsigned long)(Recipe->Amount[i] / PUMPSPEED * 1000))
      {
        digitalWrite(PumpPins[i], LOW);
        PumpState[i] = false;
      }
      else
        processtate = false;
  return processtate;
} // End of func

// This fucntion start coocking recipe
// Input: Recipe pointer
// Output: Nothing
void StartCoocking( recipe *Recipe )
{
  for (int i = 0; i < 6; i++)
    if (Recipe->Amount[i] > 0)
    {
      digitalWrite(PumpPins[i], HIGH);
      PumpState[i] = true;
    }
} // End of func

// Read data from Sd card
// Input: Nothing.
// Output: True if read was sucsses, False if not.
bool ReadSd( void )
{
  // null memory
  memset(PumpPins, sizeof(PumpPins), 0);
  memset(Resipes, sizeof(Resipes), 0);

  if (!SD.begin(chipSelect))
    return false;

  File F = SD.open("alkobot.bin", FILE_READ);

  char sign[11];
  if (F)
  {
    F.read(sign, 11);
    if (strcmp(sign, "ALCOBOTv0.3") == 0)
    {
      // Read pins
      for (int i = 0; i < 6; i++)
      {
        F.seek(F.position() + 255); // skip name
        F.read(&PumpPins[i], 1);
      }
      // Read resipies
      for (int i = 0; i < 6; i++)
      {
        F.seek(F.position() + 255); // skip name
        for (int j = 0; j < 6; j++)
        {
          F.read(&Resipes[i].Amount[j], 2);
        }
        F.read(&Resipes[i].Pin, 1);
      }
    }
    F.close();
  }

  return true;
} // End of func

void setup()
{
  pinMode(LED_BUILTIN, OUTPUT);
  
  while (ReadSd() == false)
  {
    digitalWrite(LED_BUILTIN, HIGH);   // turn the LED on (HIGH is the voltage level)
    delay(300);                       // wait for a second
    digitalWrite(LED_BUILTIN, LOW);    // turn the LED off by making the voltage LOW
    delay(300);
  } // Напищи тут какую нибудь хуйню, чтобы мы поняли, что пизда ридеру
  for (int i = 0; i < 6; i++)
    pinMode(Resipes[i].Pin, INPUT);

  for (int i = 0; i < 6; i++)
    pinMode(PumpPins[i], OUTPUT);
}

void loop()
{
  for (int i = 0; i < 6; i++)
    if (digitalRead(Resipes[i].Pin) == HIGH)
    {
      unsigned long starttime = millis();
      StartCoocking(&Resipes[i]);
      while (!ProcessFinish(starttime, &Resipes[i]))
        ;
        break;
    }
}
