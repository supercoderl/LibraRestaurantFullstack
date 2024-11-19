using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Domain.Entities;

public class Employee : Entity
{
    public Guid? StoreId { get; set; }
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Password { get; private set; }
    public string Mobile {  get; private set; }
    public UserStatus Status { get; private set; }
    public DateTime RegisteredAt { get; private set; }
    public DateTimeOffset? LastLoggedinDate { get; private set; }

    [ForeignKey("StoreId")]
    [InverseProperty("Employees")]
    public virtual Store? Store { get; set; }

    [InverseProperty("Employee")]
    public virtual ICollection<EmployeeRole>? EmployeeRoles { get; set; } = new List<EmployeeRole>();

    [InverseProperty("Employee")]
    public virtual ICollection<Token>? Tokens { get; set; } = new List<Token>();

    public string FullName => $"{FirstName}, {LastName}";

    public Employee(
        Guid id,
        Guid? storeId,
        string email,
        string firstName,
        string lastName,
        string mobile,
        string password,
        DateTime registeredAt,
        UserStatus status = UserStatus.Active) : base(id)
    {
        StoreId = storeId;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Mobile = mobile;
        Password = password;
        RegisteredAt = registeredAt;
        Status = status;
    }

    public void SetStore(Guid? storeId)
    {
        StoreId = storeId;
    }

    public void SetEmail(string email)
    {
        Email = email;
    }

    public void SetFirstName(string firstName)
    {
        FirstName = firstName;
    }

    public void SetLastName(string lastName)
    {
        LastName = lastName;
    }

    public void SetMobile(string mobile)
    {
        Mobile = mobile;
    }

    public void SetPassword(string password)
    {
        Password = password;
    }

    public void SetRegisteredAt()
    {
        RegisteredAt = DateTime.Now;
    }

    public void SetLastLoggedinDate(DateTimeOffset lastLoggedinDate)
    {
        LastLoggedinDate = lastLoggedinDate;
    }

    public void SetStatus(UserStatus status)
    {
        Status = status;
    }
}