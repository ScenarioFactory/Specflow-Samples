namespace AutoWorkshop.Specs.Screenplay.Tasks
{
    using Drivers;
    using Pattern;

    public class ViewCustomer : WebTask
    {
        private readonly int _customerId;

        private ViewCustomer(int customerId)
        {
            _customerId = customerId;
        }

        public static ViewCustomer WithId(int customerId)
        {
            return new ViewCustomer(customerId);
        }

        public override void PerformAs(IActor actor, AutoWorkshopDriver driver)
        {
            driver.NavigateTo($"custmaint.php?custid={_customerId}");
        }
    }
}
