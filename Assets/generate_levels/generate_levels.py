import random
import operator as op
from functools import partial

class InfiniteLoopException(Exception):
    pass

# for i in range(1, 15)
def bbin(x):
    return bin(x)[2:].rjust(10, '0')

symbols_used = []
seen = set()
previous = None
def random_operations(player, constant):
    global previous
    x = random.randint(1, 4)
    ops = [
        (op.xor(player, constant), "x"),
        (op.or_(player, constant), "o"),
        (op.and_(player, constant), "a"),
        (op.rshift(player, x), f"> by {x}"),
        (op.lshift(player, x), f"< by {x}")
    ]

    chosen, message=random.choice(ops)
    attempts = 0
    while (first_word := message.split()[0]) == previous or chosen in seen:
        chosen, message=random.choice(ops)
        attempts += 1
        if attempts > 20:
            raise InfiniteLoopException
        
    
    seen.add(chosen)
    if len(message.split()) > 1:
        symbols_used.extend(first_word * x)
    else:
        symbols_used.append(first_word)
    previous = first_word

    print(message)
    return chosen

while True:
    player, constant = random.randint(0, 16), random.randint(16, 100)
    symbols_used.clear()
    seen.clear()
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
    try:
        for i in range(random.randint(6, 10)):
            cur = random_operations(cur, constant)
            print(bbin(cur))
    except InfiniteLoopException:
        continue

    if bbin(cur).count("1") <=3 or len(bbin(cur)) > len(bbin(player)):
        print("skipping")
        print("$"*30)
        continue
    print()
    print("result")
    symbols_string = "".join(sorted(set(symbols_used), reverse=False))
    print(symbols_string)
    print(bbin(cur))
    print(bbin(constant))
    print(bbin(player))

    # print("=======")
    # print(bbin(constant))
    full_process = "".join(symbols_used)
    print(full_process)
    # print(bbin(player))
    keep = input()
    if keep:
        with open(r"C:\Users\cobou\Documents\C# Games\Unity\CodeAndProject\CompSciGame\Assets\StreamingAssets\bitLevels\testing.txt", 'a') as f:
            print(symbols_string, bbin(cur), f'{bbin(constant)}#{full_process}',
                  bbin(player), "", sep='\n', file=f, end='\n')

