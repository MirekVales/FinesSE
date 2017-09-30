using FinesSE.Contracts.Invokable;
using FinesSE.Expressions.Contracts;
using System;
using FinesSE.Contracts.Infrastructure;

namespace FinesSE.Outil.Expressions
{
    public class If : IWorkflowAction
    {
        public IExpressionEngine Engine { get; set; }

        public BranchType BranchType => BranchType.Open;

        public bool Evaluate(string command)
            => !Engine.Execute(command, Convert.ToBoolean);
    }
}