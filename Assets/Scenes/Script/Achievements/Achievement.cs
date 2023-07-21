using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Achievement
{
    public bool BAchieved;

    private string mExplain;
    private string mObtain;

    public virtual bool IsComplete()
    {
        return false;
    }
    public virtual void EarnRewards() { }
}

public class ReachLevel5 : Achievement
{
    public override bool IsComplete()
    {
        return GameManager.instance.Level >= 5;
    }
    public override void EarnRewards()
    {
        UserInfo.instance.UserDataSet.BUnlockAccessories[(int)Enums.EAccessory.Wings] = true;
    }
}

public class ReachLevel10 : Achievement
{
    public override bool IsComplete()
    {
        return GameManager.instance.Level >= 10;
    }
    public override void EarnRewards()
    {
        UserInfo.instance.UserDataSet.BUnlockAccessories[(int)Enums.EAccessory.Crown] = true;
    }
}

public class ReachLevel20InStage1 : Achievement
{
    public override bool IsComplete()
    {
        return GameManager.instance.GameStage == 1 && GameManager.instance.Level >= 20;
    }
    public override void EarnRewards()
    {

    }
}

public class Survive1Min : Achievement
{
    public override bool IsComplete()
    {
        return GameManager.instance.GameTime >= 60;
    }
    public override void EarnRewards()
    {

    }
}

public class Survive5MinPlayGennaro : Achievement
{
    public override bool IsComplete()
    {
        return;
    }
    public override void EarnRewards()
    {

    }
}

public class  : Achievement
{
    public override bool IsComplete()
{
    return;
}
public override void EarnRewards()
{

}
}

public override void EarnRewards()
{

}
}
