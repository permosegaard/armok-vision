﻿using System.Xml.Linq;

public class MaterialTextureConfiguration : MaterialConfiguration<int>
{
    public override bool ParseTypeElement(XElement elemtype, out int value)
    {
        XAttribute indexAtt = elemtype.Attribute("index");
        if (indexAtt == null)
        {
            //Add error message here
            value = default(int);
            return false;
        }
        return int.TryParse(indexAtt.Value, out value);
    }

    public override string rootName
    {
        get { return "materialTextures"; }
    }

    protected override string nodeName
    {
        get { return "materialTexture"; }
    }
}