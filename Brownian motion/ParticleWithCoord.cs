using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brownian_motion
{
    class ParticleWithCoord : Particle
    {
        public double[] X_array;
        public double[] Y_array;
        public int ActionTime;

        public ParticleWithCoord() : base()
        {
            X_array = new double[1];
            Y_array = new double[1];
            ActionTime = 0;

        }


        public override void Move(Size size)
        {
            base.Move( size);
            
                X_array[X_array.Length - 1] = xCoord;
                Y_array[Y_array.Length - 1] = yCoord;

                double[] just_xArr = X_array;
                double[] just_yArr = Y_array;

                X_array = new double[just_xArr.Length + 1];
                Y_array = new double[just_yArr.Length + 1];

                for (int i = 0; i < just_xArr.Length; i++)
                {
                    X_array[i] = just_xArr[i];
                    Y_array[i] = just_yArr[i];
                }
                ActionTimeInkrement();
            
        }

        private void ActionTimeInkrement()
        {
            ActionTime += 50;
        }

    }
}
