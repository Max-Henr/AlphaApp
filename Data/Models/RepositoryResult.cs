﻿
namespace Data.Models;

public class RepositoryResult<T>
{
    public bool IsSuccess { get; set; }  

    public int StatusCode { get; set; }

    public string? ErrorMessage { get; set; }

    public T? Result { get; set; }

}
