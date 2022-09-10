import random
import operator as op
from functools import partial

# for i in range(1, 15)
def bbin(x):
    return bin(x)[2:].rjust(10, '0')

def random_operations(player, constant):
    x = random.randint(1, 4)
    ops = [
        (op.xor(player, constant), "xor"),
        (op.or_(player, constant), "or_"),
        (op.and_(player, constant), "and_"),
        (op.rshift(player, x), f"rshift by {x}"),
        (op.lshift(player, x), f"lshift by {x}")
    ]
    chosen, message = random.choice(ops)
    print(message)
    return chosen

while True:
    player, constant = random.randint(0, 32), random.randint(0, 32)
    # print(bbin(a))
    # print('=========')
    # a = bbin(a))
    # printbbin(b))
    print("start:")
    print("const")
    print(bbin(constant))
    print("player")
    print(bbin(player))
    cur = player
    for i in range(random.randint(3, 7)):
        cur = random_operations(cur, constant)
        print(bbin(cur))

    print("result")
    print(bbin(cur))
    # print("=======")
    # print(bbin(constant))
    # print(bbin(player))
    input()
