### Repository structure

AutoWorkshop.Model - sample domain model to write scenarios against directly.

AutoWorkshop.Specs - sample acceptance test suite of WebDriver and domain model tests.

### SpecFlow test suite design guidelines

- Provide database and other infrastructure access via stateless repositories and services.
- Retain all SpecFlow types and parsing of SpecFlow parameters in step files.
- Avoid coupling steps to infrastructure methods. Pass distinct DTOs or primitive types between them.
- Only perform assertions in steps.
- Only perform polling for eventually consistent values in steps, not infrastructure methods.
- Use hooks very sparingly and with explicit method names and locations.
- Avoid sharing state between step files whenever possible.
- When state is shared between steps in the same file, use local member variables, not injected types.
- Where state is shared between steps, assert the state with an explicit guard in dependent steps.
