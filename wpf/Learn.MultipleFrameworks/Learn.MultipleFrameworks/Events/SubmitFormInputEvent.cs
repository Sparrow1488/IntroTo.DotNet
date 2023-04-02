using Prism.Events;

namespace Learn.MultipleFrameworks.Events;

public class SubmitFormInputEvent : PubSubEvent<FormInput>
{
    
}

public class FormInput
{
    public FormInput(string value)
    {
        Value = value;
    }
    
    public string Value { get; }
}