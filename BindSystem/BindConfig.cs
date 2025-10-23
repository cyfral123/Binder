using System;
using System.Collections.Generic;

[Serializable]
public class BindConfig
{
    public Dictionary<string, string[]> Binds { get; set; } = new Dictionary<string, string[]>();
}

