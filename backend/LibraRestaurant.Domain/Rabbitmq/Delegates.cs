using System;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Rabbitmq;

public delegate Task<bool> ConsumeEventHandler(ReadOnlyMemory<byte> content);