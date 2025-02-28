﻿namespace Ordering.Domain.ValueObjects
{
    public record OrderName
    {
        private const int DEFAULT_LENGTH = 5;
        public string Value { get; } = default!;
        private OrderName(string value) => Value = value;

        public static OrderName Of(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            //ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DEFAULT_LENGTH);

            return new OrderName(value);
        }

    }
}
