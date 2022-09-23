import random
import os
import operator as op
from math import *
from functools import partial
from collections import *

import argparse

class InfiniteLoopException(Exception):
    pass

max_digits = 11
# for i in range(1, 15)
def bbin(x):
    return bin(x)[2:].rjust(max_digits, '0')


TESTING_FILE_PATH = r"C:\Users\cobou\Documents\C# Games\Unity\CodeAndProject\CompSciGame\Assets\StreamingAssets\bitLevels\testing.txt"
symbols_used = []
seen = set()
previous = None
ones_mask = 2 ** max_digits - 1
max_steps = 8
def get_ops(player, constant, x):
    ops = [
        (op.xor(player, constant), "x"),
        (op.or_(player, constant), "o"),
        (op.and_(player, constant), "a"),
        (op.inv(player) & ones_mask, "n"),
        (op.rshift(player, x), f"> by {x}"),
        (op.lshift(player, x), f"< by {x}")
    ]
    # print(ops)
    # print(ones_mask)
    return [res for res in ops if res[0] < ones_mask]
    
def random_operations(player, constant):
    global previous, player_starting
    x = random.randint(1, 4)
    ops = get_ops(player, constant, x)

    chosen, message=random.choice(ops)
    attempts = 0
    while (first_word := message.split()[0]) == previous or player == chosen or chosen in seen or chosen >= ones_mask:
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
    if chosen == 0 and ">" in "".join(symbols_used):
        seen.clear()
        player_starting = 0
        symbols_used.clear()

    print(message)
    return chosen

def bfs(goal, const, start, symbols, limit=None):
    fringe = deque([(start, "")])
    seen = set()
    while fringe:
        cur, steps = fringe.popleft()
        if cur in seen:
            continue
        seen.add(cur)
        if cur == goal:
            return (cur, steps)
        for result, symbol in get_ops(cur, const, 1):
            letter = symbol[0]
            if letter in symbols:
                fringe.append((result, steps + letter))
        
def get_symbol_string(x):
    return "".join(sorted(set(x), reverse=False))
def main(args):
    global player_starting, symbols_used
    while True:
        player_starting, constant = random.randint(0, 16), random.randint(16, ones_mask)
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
        print(bbin(player_starting))
        cur = player_starting
        try:
            for i in range(random.randint(6, 6)):
                cur = random_operations(cur, constant)
                print(bbin(cur))
        except InfiniteLoopException:
            continue

        symbols_string = get_symbol_string(symbols_used)
        if bbin(cur).count("1") <=3 or len(bbin(cur)) > len(bbin(player_starting)):
            print("skipping")
            print("$"*30)
            continue
        bfs_result = bfs(cur, constant, player_starting, symbols_string)
        print()
        print("bfs way", bfs_result)
        print("result")
        print(symbols_string)
        print(bbin(cur))
        print(bbin(constant))
        print(bbin(player_starting))
        # print("=======")
        # print(bbin(constant))
        full_process = "".join(symbols_used)
        print(full_process)
        if len(bfs_result[1]) < len(full_process):
            print("using bfs way for shorter path")
            full_process = bfs_result[1]
            symbols_string = get_symbol_string(full_process)
            print(full_process)
        else:
            full_process = "".join(symbols_used)
        # print(bbin(player))
        keep = input()
        if keep:
            with open(TESTING_FILE_PATH, 'a') as f:
                print(f"{len(full_process) if args.force else ''}{symbols_string}", bbin(cur), f'{bbin(constant)}#{full_process}',
                    bbin(player_starting), "", sep='\n', file=f, end='\n')

if __name__ == '__main__':
    parser = argparse.ArgumentParser()
    parser.add_argument("-f", "--force", help="should the shortest path be forced?", action="store_true")
    args = parser.parse_args()
    try:
        main(args)
    except KeyboardInterrupt as e:
        print(e)
        os.system(f'dos2unix "{TESTING_FILE_PATH}"')
        
