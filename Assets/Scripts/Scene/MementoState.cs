using System.Collections.Generic;

// Lidia Paiva
public class MementoState
{
    private List<ParamsMemento> _parameters = new List<ParamsMemento>();

    public void Rec(params object[] parameter)
    {
        if (_parameters.Count >= 1000)
            _parameters.RemoveAt(0);

        var remember = new ParamsMemento(parameter);
        _parameters.Add(remember);
    }

    public bool IsRemember()
    {
        return _parameters.Count > 0;
    }

    public ParamsMemento Remember()
    {
        var x = _parameters[_parameters.Count - 1];
        _parameters.RemoveAt(_parameters.Count - 1);

        return x;
    }
}