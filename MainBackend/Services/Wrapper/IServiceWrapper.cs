﻿using MainBackend.Services.Interfaces;

namespace MainBackend.Services.Wrapper;

public interface IServiceWrapper
{
    IGeneratorService generator { get; }
    ILanesService lane { get; }
    IPersonService person { get; }
    IReservationService reservation { get; }
    IUserService user { get; }
    IWorkerService worker { get; }
    IWorkScheduleService workSchedule { get; }
}