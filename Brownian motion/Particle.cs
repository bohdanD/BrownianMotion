using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brownian_motion
{
    class Particle
    {
        protected double xCoord;
        protected double yCoord;
        protected double xSpeed;
        protected double ySpeed;
        protected double maxSpeed;
    

        public Particle()
        {
            
        }

        public double SetMaxSpeed
        {
            set { maxSpeed = value; }
        }
        public double X
        {
            get { return xCoord; }
            set { xCoord = value; }
        }
        public double Y
        {
            get { return yCoord; }
            set { yCoord = value; }
        }
        public double X_Speed
        {
            get { return xSpeed; }
            set { xSpeed = value; }

        }
        public double Y_Speed
        {
            get { return ySpeed; }
            set { ySpeed = value; }
        }


        public virtual void Move(Size size)
        {
            this.xCoord += xSpeed;
            yCoord += ySpeed;
            if(xCoord>= size.Width)
            {
                xCoord-=xSpeed;
                xSpeed = -xSpeed;
            }
            if (xCoord < 1)
            {
                xCoord -= xSpeed;
                xSpeed = -xSpeed;
            }
            if (yCoord >= size.Height)
            {
                yCoord -= ySpeed;
                ySpeed = -ySpeed;
            }
            if (yCoord < 1)
            {
                yCoord -= ySpeed;
                ySpeed = -ySpeed;
            }
        }
       

        public void repulsionPower(Particle [] array)
        {
           
                foreach (Particle i in array)
                {
                    if (!this.Equals(i))
                    {
                        var d = Math.Sqrt((this.xCoord - i.xCoord) * (this.xCoord - i.xCoord) +
                            (this.yCoord - i.yCoord) * (this.yCoord - i.yCoord));
                      
                        if (d <= 10)
                        {
                           
                            xSpeed += (xCoord - i.xCoord) / 8;
                            ySpeed += (yCoord - i.yCoord) / 8;
                            i.xSpeed -= (xCoord - i.xCoord) / 8;
                            i.ySpeed -= (yCoord - i.yCoord) / 8;
                        if (xSpeed > maxSpeed)
                            xSpeed = maxSpeed;
                        if (xSpeed < -maxSpeed)
                            xSpeed = -maxSpeed;
                        if (ySpeed > maxSpeed)
                            ySpeed = maxSpeed;
                        if (ySpeed < -maxSpeed)
                            ySpeed = -maxSpeed;


                        }
                    }
                }
            
        }
    }
}
