﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace autonomiczny_samochod
{
    //needed for evTargetSpeedChanged
    public delegate void TargetSpeedChangedEventHandler(object sender, TargetSpeedChangedEventArgs args);
    public class TargetSpeedChangedEventArgs : EventArgs
    {
        private double targetSpeed;
        public TargetSpeedChangedEventArgs(double speed)
        {
            targetSpeed = speed;
        }

        public double GetTargetSpeed()
        {
            return targetSpeed;
        }
    }

    //needed for evTargetSteeringWheelAngleChanged
    public delegate void TargetSteeringWheelAngleChangedEventHandler(object sender, TargetSteeringWheelAngleChangedEventArgs args);
    public class TargetSteeringWheelAngleChangedEventArgs
    {
        private double targetAngle;
        public TargetSteeringWheelAngleChangedEventArgs(double angle)
        {
            targetAngle = angle;
        }

        public double GetTargetWheelAngle()
        {
            return targetAngle;
        }
    }

    public interface ICar
    {
        event EventHandler evAlertBrake;
        event TargetSpeedChangedEventHandler evTargetSpeedChanged;
        event TargetSteeringWheelAngleChangedEventHandler evTargetSteeringWheelAngleChanged;

        ISteeringWheelAngleRegulator SteeringWheelAngleRegulator
        {
            get;
        }
        ISpeedRegulator SpeedRegulator
        {
            get;
        }
        CarController Controller
        {
            get;
        }
        ICarCommunicator CarComunicator
        {
            get;
        }

        CarInformations CarInfo
        {
            get;
        }

        IBrakeRegulator BrakeRegulator
        {
            get;
        }
    
        void SetTargetWheelAngle(double targetAngle);

        void SetTargetSpeed(double targetSpeed);

        void ActivateAlertBrake();

        void OverrideTargetBrakeSetting(double setting);

        void EndTargetBrakeSteeringOverriding();
    }
}
