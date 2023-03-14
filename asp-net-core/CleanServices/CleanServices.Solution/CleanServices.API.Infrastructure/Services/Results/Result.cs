namespace CleanServices.API.Infrastructure.Services.Results;

public class Result
{
    public Result(Exception ex)
    {
        Exception = ex;
        Status = ResultStatus.Failure;
    }

    public Result()
    {
        Status = ResultStatus.Success;
    }
    
    public Exception? Exception { get; }
    public ResultStatus Status { get; }
}

public class Result<TEntity> : Result
{
    public Result(Exception ex) : base(ex)
    {
        
    }

    public Result(TEntity entity)
    {
        Entity = entity;
    }

    public TEntity Entity { get; }
}