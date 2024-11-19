using System.Net.NetworkInformation;

namespace LibraRestaurant.Domain.Errors;

public static class DomainErrorCodes
{
    public static class Employee
    {
        // Employee Validation
        public const string EmptyId = "EMPLOYEE_EMPTY_ID";
        public const string EmptyFirstName = "EMPLOYEE_EMPTY_FIRST_NAME";
        public const string EmptyLastName = "EMPLOYEE_EMPTY_LAST_NAME";
        public const string EmptyMobile = "EMPLOYEE_EMPTY_MOBILE";
        public const string EmailExceedsMaxLength = "EMPLOYEE_EMAIL_EXCEEDS_MAX_LENGTH";
        public const string FirstNameExceedsMaxLength = "EMPLOYEE_FIRST_NAME_EXCEEDS_MAX_LENGTH";
        public const string LastNameExceedsMaxLength = "EMPLOYEE_LAST_NAME_EXCEEDS_MAX_LENGTH";
        public const string MobileExceedsMaxLength = "EMPLOYEE_MOBILE_EXCEEDS_MAX_LENGTH";
        public const string InvalidEmail = "EMPLOYEE_INVALID_EMAIL";

        // Employee Password Validation
        public const string EmptyPassword = "EMPLOYEE_PASSWORD_MAY_NOT_BE_EMPTY";
        public const string ShortPassword = "EMPLOYEE_PASSWORD_MAY_NOT_BE_SHORTER_THAN_6_CHARACTERS";
        public const string LongPassword = "EMPLOYEE_PASSWORD_MAY_NOT_BE_LONGER_THAN_50_CHARACTERS";
        public const string UppercaseLetterPassword = "EMPLOYEE_PASSWORD_MUST_CONTAIN_A_UPPERCASE_LETTER";
        public const string LowercaseLetterPassword = "EMPLOYEE_PASSWORD_MUST_CONTAIN_A_LOWERCASE_LETTER";
        public const string NumberPassword = "EMPLOYEE_PASSWORD_MUST_CONTAIN_A_NUMBER";
        public const string SpecialCharPassword = "EMPLOYEE_PASSWORD_MUST_CONTAIN_A_SPECIAL_CHARACTER";

        // General
        public const string AlreadyExists = "EMPLOYEE_ALREADY_EXISTS";
        public const string PasswordIncorrect = "EMPLOYEE_PASSWORD_INCORRECT";
    }

    public static class MenuItem
    {
        // Item Validation
        public const string EmptyId = "ITEM_EMPTY_ID";
        public const string EmptyTitle = "ITEM_EMPTY_TITLE";
        public const string EmptySlug = "ITEM_EMPTY_SLUG";
        public const string EmptySKU = "ITEM_EMPTY_SKU";
        public const string EmptyPrice = "ITEM_EMPTY_PRICE";
        public const string EmptyQuantity = "ITEM_EMPTY_QUANTITY";
        public const string TitleExceedsMaxLength = "ITEM_TITLE_EXCEEDS_MAX_LENGTH";
        public const string SlugExceedsMaxLength = "ITEM_SLUG_EXCEEDS_MAX_LENGTH";
        public const string SKUExceedsMaxLength = "ITEM_SKU_EXCEEDS_MAX_LENGTH";

        // General
        public const string AlreadyExists = "ITEM_ALREADY_EXISTS";
    }

    public static class Menu
    {
        // Menu Validation
        public const string EmptyId = "MENU_EMPTY_ID";
        public const string EmptyName = "MENU_EMPTY_TITLE";
        public const string EmptyStrore = "MENU_EMPTY_STORE";

        // General
        public const string AlreadyExists = "MENU_ALREADY_EXISTS";
    }

    public static class Category
    {
        // Menu Validation
        public const string EmptyId = "CATEGORY_EMPTY_ID";
        public const string EmptyName = "CATEGORY_EMPTY_TITLE";

        // General
        public const string AlreadyExists = "CATEGORY_ALREADY_EXISTS";
    }

    public static class Currency
    {
        // Menu Validation
        public const string EmptyId = "CURRENCY_EMPTY_ID";
        public const string EmptyName = "CURRENCY_EMPTY_TITLE";

        // General
        public const string AlreadyExists = "CURRENCY_ALREADY_EXISTS";
    }

    public static class Order
    {
        // Order Validation
        public const string EmptyId = "ORDER_EMPTY_ID";
        public const string EmptyOrderNo = "ORDER_EMPTY_ORDER_NO";
        public const string EmptyStore = "ORDER_EMPTY_STORE_ID";
        public const string EmptyServant = "ORDER_EMPTY_SERVANT_ID";
        public const string EmptyCashier = "ORDER_EMPTY_CASHIER_ID";
        public const string EmptyReservation = "ORDER_EMPTY_RESERVATION_ID";
        public const string EmptyPriceCalculated = "ORDER_EMPTY_PRICE_CALCULATED";
        public const string EmptySubtotal = "ORDER_EMPTY_SUBTOTAL";
        public const string EmptyTax = "ORDER_EMPTY_TAX";
        public const string EmptyTotal = "ORDER_EMPTY_TOTAL";

        // General
        public const string AlreadyExists = "ORDER_ALREADY_EXISTS";
    }

    public static class Store
    {
        // Store Validation
        public const string EmptyId = "STORE_EMPTY_ID";
        public const string EmptyName = "STORE_EMPTY_NAME";
        public const string EmptyCity = "STORE_EMPTY_CITY_ID";
        public const string EmptyDistrict = "STORE_EMPTY_DISTRICT_ID";
        public const string EmptyWard = "STORE_EMPTY_WARD_ID";
        public const string EmptyAddress = "STORE_EMPTY_ADDRESS";

        // General
        public const string AlreadyExists = "STORE_ALREADY_EXISTS";
    }

    public static class Reservation
    {
        // Reservation Validation
        public const string EmptyId = "RESERVATION_EMPTY_ID";
        public const string EmptyTableNumber = "RESERVATION_EMPTY_TABLE_NUMBER";
        public const string EmptyCapacity = "RESERVATION_EMPTY_CAPACITY";
        public const string EmptyStore = "RESERVATION_EMPTY_STORE_ID";
        public const string EmptyCustomerName = "RESERVATION_EMPTY_CUSTOMER_NAME";
        public const string EmptyCustomerPhone = "RESERVATION_EMPTY_CUSTOMER_PHONE";

        // General
        public const string AlreadyExists = "RESERVATION_ALREADY_EXISTS";
    }

    public static class OrderLine
    {
        // Order Line Validation
        public const string EmptyId = "ORDERLINE_EMPTY_ID";
        public const string EmptyOrder = "ORDERLINE_EMPTY_ORDER_ID";
        public const string EmptyItem = "ORDERLINE_EMPTY_ITEM_ID";
        public const string EmptyQuantity = "ORDERLINE_EMPTY_QUANTITY";

        // General
        public const string AlreadyExists = "ORDERLINE_ALREADY_EXISTS";
    }

    public static class PaymentMethod
    {
        // Payment Method Validation
        public const string EmptyId = "PAYMENT_METHOD_EMPTY_ID";
        public const string EmptyName = "PAYMENT_METHOD_EMPTY_TITLE";

        // General
        public const string AlreadyExists = "PAYMENT_METHOD_ALREADY_EXISTS";
    }

    public static class PaymentHistory
    {
        // Payment History Validation
        public const string EmptyId = "PAYMENT_HISTORY_EMPTY_ID";
        public const string EmptyTransaction = "PAYMENT_HISTORY_EMPTY_TRANSACTION";
        public const string EmptyOrder = "PAYMENT_HISTORY_EMPTY_ORDER";
        public const string EmptyPaymentMethod = "PAYMENT_HISTORY_EMPTY_PAYMENT_METHOD";
        public const string EmptyAmount = "PAYMENT_HISTORY_EMPTY_AMOUNT";

        // General
        public const string AlreadyExists = "PAYMENT_HISTORY_ALREADY_EXISTS";
    }

    public static class CategoryItem
    {
        // Category Item Validation
        public const string EmptyId = "CATEGORY_ITEM_EMPTY_ID";

        // General
        public const string AlreadyExists = "CATEGORY_ITEM_ALREADY_EXISTS";
    }

    public static class Role
    {
        // Role Validation
        public const string EmptyId = "ROLE_EMPTY_ID";
        public const string EmptyName = "ROLE_EMPTY_NAME";
        public const string EmptyEmployee = "ROLE_EMPTY_EMPLOYEE";

        // General
        public const string AlreadyExists = "ROLE_ALREADY_EXISTS";
    }

    public static class Token
    {
        // Token Validation
        public const string EmptyId = "TOKEN_EMPTY_ID";
        public const string EmptyOldToken = "TOKEN_EMPTY_OLD_TOKEN";
        public const string EmptyEmployee = "TOKEN_EMPTY_EMPLOYEE";

        // General
        public const string AlreadyExists = "TOKEN_ALREADY_EXISTS";
    }

    public static class Message
    {
        //Message Validation
        public const string EmptyId = "MESSAGE_EMPTY_ID";
        public const string EmptyContent = "MESSAGE_CONTENT_EMPTY";
        public const string EmptyTime = "MESSAGE_TIME_EMPTY";
        public const string EmptyMessageType = "MESSAGE_TYPE_EMPTY";
    }

    public static class Discount
    {
        //Discount Validation
        public const string EmptyId = "DISCOUNT_EMPTY_ID";
    }

    public static class DiscountType
    {
        //DiscountType Validation
        public const string EmptyId = "DISCOUNT_TYPE_EMPTY_ID";
        public const string EmptyName = "DISCOUNT_TYPE_EMPTY_NAME";
        public const string EmptyValue = "DISCOUNT_TYPE_EMPTY_VALUE";
        public const string EmptyMinOrderValue = "DISCOUNT_TYPE_EMPTY_MIN_ORDER_VALUE";
        public const string EmptyMinItemQuantity = "DISCOUNT_TYPE_MIN_ITEM_QUANTITY";
        public const string EmptyMaxDiscountValue = "DISCOUNT_TYPE_MAX_DISCOUNT_VALUE";
    }

    public static class Review
    {
        //Review Validation
        public const string EmptyId = "REVIEW_TYPE_EMPTY_ID";
        public const string EmptyName = "REVIEW_EMPTY_CUSTOMER_NAME";
        public const string EmptyRating = "REVIEW_EMPTY_RATING";
        public const string EmptyComment = "REVIEW_EMPTY_COMMENT";
    }

    public static class Customer
    {
        //Customer Validation
        public const string EmptyId = "CUSTOMER_TYPE_EMPTY_ID";
        public const string EmptyName = "CUSTOMER_EMPTY_NAME";
        public const string EmptyPhone = "CUSTOMER_EMPTY_PHONE";
    }

    public static class Google
    {
        //Google Validation
        public const string EmptyEmail = "GOOGLE_TYPE_EMPTY_EMAIL";
    }
}