namespace NCourseWork.Persistence.Common.Compiler
{
    internal interface ISqlCompilerProvider
    {
        public SqlKata.Compilers.Compiler GetCompiler();
    }
}
