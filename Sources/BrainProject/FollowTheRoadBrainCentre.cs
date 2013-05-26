﻿using Helpers;
using CarController;
using VisionFilters;


using VisionFilters.Output;


namespace BrainProject
{
    

    public class FollowTheRoadBrainCentre
    {
        private DefaultCarController carController;
        private PIDRegulator regulator;
        private RoadCenterDetector roadDetector;

        public delegate void newTargetWheelAngeCalculatedEventHandler(object sender, double angle);
        public event newTargetWheelAngeCalculatedEventHandler evNewTargetWheelAngeCalculated;

        public delegate void newTargetSpeedCalculatedEventHandler(object sender, double speed);
        public event newTargetSpeedCalculatedEventHandler evNewTargetSpeedCalculated;

        private const double AUTOMATIC_DRIVING_SPEED = System.Math.PI;

        private const int TIME_TO_STOP_CAR_WHEN_NO_INFO_RECEIVED_IN_MS = 1500;

        /// <summary>
        /// timer which makes car to stop automaticly when new data is not received for some time
        /// </summary>
        private System.Timers.Timer autoStopTimer;

        class Settings : PIDSettings
        {
            public Settings()
            {
                //P part settings
                P_FACTOR_MULTIPLER = 0.2;

                //I part settings
                I_FACTOR_MULTIPLER = 0.1; 
                I_FACTOR_SUM_MAX_VALUE = 100;
                I_FACTOR_SUM_MIN_VALUE = -100;
                I_FACTOR_SUM_SUPPRESSION_PER_SEC = 0.4; //very strong suppression //1.0 = suppresing disabled

                //D part settings
                D_FACTOR_MULTIPLER = 0.0;
                D_FACTOR_SUPPRESSION_PER_SEC = 0.0; 
                D_FACTOR_SUM_MIN_VALUE = 100;
                D_FACTOR_SUM_MAX_VALUE = -100;

                //steering limits
                MAX_FACTOR_CONST = 30;
                MIN_FACTOR_CONST = -30;
            }
        }

        public FollowTheRoadBrainCentre(RoadCenterDetector _roadDetector, DefaultCarController _carController)
        {
            roadDetector = _roadDetector;

            carController = _carController;
            carController.SetTargetSpeed(0);
            carController.SetTargetWheelAngle(0);

            regulator = new PIDRegulator(new Settings(), "carSteeringRegulator");
            regulator.SetTargetValue(0.0); //we want to go straight with the road

            autoStopTimer = new System.Timers.Timer();
            autoStopTimer.Elapsed += new System.Timers.ElapsedEventHandler(autoStopTimer_Elapsed);

            roadDetector.RoadCenterSupply += new RoadCenterHandler(roadDetector_RoadCenterSupply);
        }

        void autoStopTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            newTargetSpeedCalculatedEventHandler temp = evNewTargetSpeedCalculated;
            if (temp != null)
            {
                temp(this, 0);
            }
        }

        double[] pointsWages = new double[3] { 1.0, 1.0, 1.0 };
        void roadDetector_RoadCenterSupply(object sender, RoadCenterEvent e)
        {
            double currentValue = 0;
            for (int i = 0; i < 3; i++)
            {
                currentValue = (e.road[i].X - CamModel.Width / 2) * pointsWages[i] * -1;
            }
            if (Limiter.LimitAndReturnTrueIfLimitted(ref currentValue, -100, 100))
            {
                Logger.Log(this, "road value has been limmited", 1);
            }

            double steeringVal = regulator.ProvideObjectCurrentValueToRegulator(currentValue);

            carController.SetTargetWheelAngle(steeringVal);

            newTargetWheelAngeCalculatedEventHandler temp = evNewTargetWheelAngeCalculated;
            if (temp != null)
            {
                temp(this, steeringVal);
            }

            newTargetSpeedCalculatedEventHandler newSpeedEv = evNewTargetSpeedCalculated;
            if (newSpeedEv != null)
            {
                newSpeedEv(this, AUTOMATIC_DRIVING_SPEED);
            }

            autoStopTimer.Interval = TIME_TO_STOP_CAR_WHEN_NO_INFO_RECEIVED_IN_MS;
            autoStopTimer.Start();
            
        }

    }
}
