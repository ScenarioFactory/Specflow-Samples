﻿namespace AutoWorkshop.Specs.Screenplay.Pattern
{
    public interface IQuestion<out TAnswer>
    {
        TAnswer AskAs(IActor actor);
    }
}
