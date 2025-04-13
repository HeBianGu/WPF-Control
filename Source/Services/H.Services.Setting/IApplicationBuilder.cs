namespace System;

public interface IApplicationBuilder
{

}

public interface IConfigureableApplication
{
    void Configure();
}

public interface ILoginableApplication
{
    void Login();
}
