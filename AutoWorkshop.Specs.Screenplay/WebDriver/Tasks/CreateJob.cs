namespace AutoWorkshop.Specs.Screenplay.WebDriver.Tasks
{
    using System;
    using Pages;
    using Pattern;

    public class CreateJob : WebTask
    {
        private readonly string _description;
        private DateTime _date;
        private decimal _hours;
        private int _mileage;

        private CreateJob(string description)
        {
            _description = description;
        }

        public static CreateJob WithDescription(string description)
        {
            return new CreateJob(description);
        }

        public CreateJob OnDate(DateTime date)
        {
            _date = date;
            return this;
        }

        public CreateJob TakingHours(decimal hours)
        {
            _hours = hours;
            return this;
        }

        public CreateJob AtMileage(int mileage)
        {
            _mileage = mileage;
            return this;
        }

        protected override void PerformAs(IActor actor, AutoWorkshopDriver driver)
        {
            actor.AttemptsTo(
                SendKeys.To(JobMaintenancePage.Description, _description),
                ChooseDate.For(JobMaintenancePage.Start, _date),
                SendKeys.To(JobMaintenancePage.Hours, _hours.ToString("0.##")),
                SendKeys.To(JobMaintenancePage.Mileage, _mileage.ToString()),
                Click.On(JobMaintenancePage.Save));
        }
    }
}
