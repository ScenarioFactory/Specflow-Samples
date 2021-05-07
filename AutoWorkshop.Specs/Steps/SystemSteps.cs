namespace AutoWorkshop.Specs.Steps
{
    using System;
    using Framework;
    using Repositories;
    using TechTalk.SpecFlow;

    [Binding]
    public class SystemSteps
    {
        private readonly OverrideDateRepository _overrideDateRepository;

        public SystemSteps(OverrideDateRepository overrideDateRepository)
        {
            _overrideDateRepository = overrideDateRepository;
        }

        [Given(@"the date is '(.*)'")]
        public void GivenTheDateIs(ScenarioDate currentDate)
        {
            _overrideDateRepository.SetCurrentDate(currentDate);
        }
    }
}
