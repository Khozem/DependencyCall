// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

new ExecuteDependency().ExecuteAllClasses(new Dependencies());

public class ExecuteDependency
{

    public List<Ibaseclass> AlreadyExecuted = new List<Ibaseclass>();    

    public void ExecuteAllClasses(Dependencies allclasseswithdep)
    {        
        var classToExecute = allclasseswithdep.GetDependencies();

        foreach (var item in classToExecute)
        {
            if(!AlreadyExecuted.Contains(item.Key))
                ExecuteClassAndItsDependency(item.Key, item.Value, classToExecute);
        }

    }

    public void ExecuteClassAndItsDependency(Ibaseclass cls, List<Ibaseclass> clsDependencies, Dictionary<Ibaseclass, List<Ibaseclass>> allClasses)
    {   
        if (clsDependencies != null)
        {
            foreach (var depenCls in clsDependencies)
            {
                var clsDetail = allClasses[depenCls];
                ExecuteClassAndItsDependency(depenCls, clsDetail, allClasses);
            }
        }
        

        if (CheckAllTheDependenciesAreExecuted(cls, allClasses) && !AlreadyExecuted.Contains(cls))
        {
            cls.execute();
            AlreadyExecuted.Add(cls);
        }
    }

    public bool CheckAllTheDependenciesAreExecuted(Ibaseclass cls, Dictionary<Ibaseclass, List<Ibaseclass>> allClasses)
    {

        var clsDetail = allClasses[cls];
        if (clsDetail != null)
        {
            foreach (var depCls in clsDetail)
            {
                if (!AlreadyExecuted.Contains(depCls))
                    return false;
            }
        }

        return true;
    }

}




public interface Ibaseclass
{
    public int classId { get; }
    public void execute();
}


public class class1 : Ibaseclass
{

    public int classId { get { return 1; } }    

    public override int GetHashCode()
    {
        return classId;
    }

    public override bool Equals(object? obj)
    {
        return ((Ibaseclass)obj).classId == this.classId;
    }

    public void execute()
    {
        Console.WriteLine("Class1: Had no dependency");
    }
}

public class class2 : Ibaseclass
{
    public int classId { get { return 2; } }
    public override int GetHashCode()
    {
        return classId;
    }

    public override bool Equals(object? obj)
    {
        return ((Ibaseclass)obj).classId == this.classId;
    }
    // depends on class1
    public void execute()
    {
        Console.WriteLine("Class2: Had dependency on class 1 and class4");
    }
}

public class class3 : Ibaseclass
{
    public int classId { get { return 3; } }
    public override int GetHashCode()
    {
        return classId;
    }

    public override bool Equals(object? obj)
    {
        return ((Ibaseclass)obj).classId == this.classId;
    }

    // depends on class1
    // depends on class2
    public void execute()
    {
        Console.WriteLine("Class3: Had dependency on class1 and class2");
    }
}

public class class4 : Ibaseclass
{
    public int classId { get { return 4; } }
    public override int GetHashCode()
    {
        return classId;
    }

    public override bool Equals(object? obj)
    {
        return ((Ibaseclass)obj).classId == this.classId;
    }

    // depends on class1
    // depends on class2
    public void execute()
    {
        Console.WriteLine("Class4: Had dependency on class5");
    }
}

public class class5 : Ibaseclass
{
    public int classId { get { return 5; } }
    public override int GetHashCode()
    {
        return classId;
    }

    public override bool Equals(object? obj)
    {
        return ((Ibaseclass)obj).classId == this.classId;
    }

    // depends on class1
    // depends on class2
    public void execute()
    {
        Console.WriteLine("Class5: Had no dependency");
    }
}

public class Dependencies
{
    public Dictionary<Ibaseclass, List<Ibaseclass>> GetDependencies()
    {
        List<Ibaseclass> dependencies = new List<Ibaseclass>();
        Dictionary<Ibaseclass, List<Ibaseclass>> classcollection = new Dictionary<Ibaseclass, List<Ibaseclass>>();

        classcollection.Add(new class1(), null);
        classcollection.Add(new class2(), new List<Ibaseclass> { new class1(), new class4() });
        classcollection.Add(new class3(), new List<Ibaseclass> { new class1(), new class2() });
        classcollection.Add(new class4(), new List<Ibaseclass> { new class5() });
        classcollection.Add(new class5(), new List<Ibaseclass> { new class1() });

        return classcollection;
    }
}