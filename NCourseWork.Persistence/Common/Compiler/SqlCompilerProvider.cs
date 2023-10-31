namespace NCourseWork.Persistence.Common.Compiler
{
    using SqlKata.Compilers;

    public class SqlCompilerProvider : ISqlCompilerProvider
    {
        private readonly Compiler compiler;

        public SqlCompilerProvider()
        {
            compiler = new SqlServerCompiler();
        }

        public Compiler GetCompiler()
        {
            return compiler;
        }
    }
}
