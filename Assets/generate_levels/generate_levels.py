import random
import operator as op
from functools import partial

# for i in range(1, 15)
def bbin(x):
    return bin(x)[2:].rjust(10, '0')

previous = None
def random_operations(player, constant):
    global previous
    x = random.randint(1, 4)
    ops = [
        (op.xor(player, constant), "xor"),
        (op.or_(player, constant), "or_"),
        (op.and_(player, constant), "and_"),
        (op.rshift(player, x), f"rshift by {x}"),
        (op.lshift(player, x), f"lshift by {x}")
    ]

    chosen, message=random.choice(ops)

    while message.split()[0] == previous:
        chosen, message=random.choice(ops)
    previous = message.split()[0]

    print(message)
    return chosen

while True:
    player, constant = random.randint(0, 16), random.randint(16, 100)
    # print(bbin(a))
    # print('=========')
    # a = bbin(a))

    # printbbin(b))
    print("========"*6)
    print("start:")
    print("const")
    print(bbin(constant))
    print("player")
    print(bbin(player))
    cur = player
    for i in range(random.randint(6, 10)):
        cur = random_operations(cur, constant)
        print(bbin(cur))
    if bbin(cur).count("1") <=3:
        continue
    print()
    print("result")
    print(bbin(cur))
    print(bbin(constant))
    print(bbin(player))

    # print("=======")
    # print(bbin(constant))
    # print(bbin(player))
    keep = input()
    if keep:
        with open("keepers.txt", 'a') as f:
            print(bbin(cur), bbin(constant), bbin(player), sep='\n',file=f)
