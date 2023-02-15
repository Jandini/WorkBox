internal interface IMain
{
#if (simple)
    void Run();
#else
    void Run(string path);
#endif
}
