
const int NONE = -1;
const int CLOSED = 0;
const int OPENED  = 1;
int trigger_pin = 2;

int echo_pin = 3;

int time;

int distance;

int door_status = NONE;




void setup ( ) {

        Serial.begin (9600); 

        pinMode (trigger_pin, OUTPUT); 

        pinMode (echo_pin, INPUT);
}




void loop ( ) {

    digitalWrite (trigger_pin, HIGH);

    delayMicroseconds (10);

    digitalWrite (trigger_pin, LOW);

    time = pulseIn (echo_pin, HIGH);

    distance = (time * 0.034) / 2;

  if (distance <= 20) 
  {

        if (door_status == NONE)
            door_status = OPENED;
        else if (door_status == CLOSED){
                  Serial.println (1); 
                  door_status = OPENED;
          }    
        delay (500);

  }

  else {

        if (door_status == NONE)
            door_status = CLOSED;
        else if (door_status == OPENED) {
                  Serial.println (0); 
                  door_status = CLOSED;
        }       
        delay (500);        

  } 

  }
