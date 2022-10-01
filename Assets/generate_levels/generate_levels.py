import random
import os
import operator as op
from math import *
from functools import partial
from collections import *
import sys

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
letter_symbols = list("xoan><")
# allowed = {

# }
def get_ops(player, constant, shift_amount):
    allowed = args.symbols_allowed
    # print("allowed", allowed)
    # if allowed is None:
    # else
    ops = [
        (op.xor(player, constant), "x"),
        (op.or_(player, constant), "o"),
        (op.and_(player, constant), "a"),
        (op.inv(player) & ones_mask, "n"),
        (op.rshift(player, shift_amount), f"> by {shift_amount}"),
        (op.lshift(player, shift_amount), f"< by {shift_amount}")
    ]
    # print(ops)
    # print(ones_mask)
    # print("allowed", allowed)
    r =  [res for res in ops if res[0] < ones_mask and res[1][0] in allowed]
    # print("returning ", r)
    return r
    
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

    # print(message)
    return chosen

def bfs(goal, const, start, symbols, limit=None):
    global args
    fringe = deque([(start, "")])
    seen = set()
    # print("upper moves is", args.upper_moves)
    while fringe:
        cur, steps = fringe.popleft()
        if cur in seen :
            continue
        seen.add(cur)
        if cur == goal or len(steps) == limit:
            return (cur, steps)
        for result, symbol in get_ops(cur, const, 1):
            letter = symbol[0]
            if letter in symbols:
                fringe.append((result, steps + letter))
    # assert False
        
def get_symbols_available_string(x):
    return "".join(sorted(set(x), reverse=False))
def main(args):
    global player_starting, symbols_used
    while True:
        player_starting, constant = random.randint(0, 64), random.randint(1, ones_mask)
        symbols_used.clear()
        seen.clear()
        # print(bbin(a))
        # print('=========')
        # a = bbin(a))

        # print("========"*6)
        # print("start:")
        # print("const")
        # print(bbin(constant))
        # print("player")
        # print(bbin(player_starting))
        cur = player_starting
        try:
            for i in range(random.randint(6, 12)):
                cur = random_operations(cur, constant)
                # print(bbin(cur))
        except InfiniteLoopException:
            continue

        symbols_string = get_symbols_available_string(symbols_used)
        if bbin(cur).count("1") <=3 or len(bbin(cur)) > len(bbin(player_starting)):
            # print("skipping")
            # print("$"*30)
            continue
        bfs_result = bfs(cur, constant, player_starting, symbols_string)
        # print()
        # print("bfs way", bfs_result)
        # print("result")
        # print(symbols_string)
        # print(bbin(cur))
        # print(bbin(constant))
        # print(bbin(player_starting))
        full_process = "".join(symbols_used)
        # print(full_process)
        if len(bfs_result[1]) < len(full_process):
            # print("using bfs way for shorter path")
            full_process = bfs_result[1]
            symbols_string = get_symbols_available_string(full_process)
            # print(full_process)
        else:
            full_process = "".join(symbols_used)
        # print(bbin(player))
        symbol_range = range(args.lo_symbols, args.max_symbols + 1)
        move_range = range(args.lo_moves, args.upper_moves + 1)
        # print("symbol_range",  symbol_range)
        # print("move_range",  move_range)
        if len(symbols_string) not in symbol_range or len(full_process) not in move_range:

            continue
        print("found one")
        if (args.num_puzzles_to_gen != -1) or input():
            with open(TESTING_FILE_PATH, 'a') as f:
                for out in [f, sys.stdout]:
                    print(f"{len(full_process) if args.force else ''}{symbols_string}", 
                    bbin(cur), 
                    f'{bbin(constant)}#{full_process}',
                    bbin(player_starting), "", 
                    sep='\n', file=out, end='\n')
            if args.num_puzzles_to_gen > 0:
                args.num_puzzles_to_gen -= 1
                if args.num_puzzles_to_gen == 0:
                    break

if __name__ == '__main__':
    parser = argparse.ArgumentParser()
    parser.add_argument("-f", "--force", help="should the shortest path be forced?", action="store_true")
    parser.add_argument("-p", "--num_puzzles_to_gen", help="total number of puzzles to generate?", type=int, default=-1)
    parser.add_argument("-l", "--lo_symbols", help="min number of symbols allowed?", type=int, default=1)
    parser.add_argument("-m", "--max_symbols", help="max number of symbols allowed?", type=int, default=6)
    parser.add_argument("-v", "--lo_moves", help="min number of moves allowed?", type=int, default=1)
    parser.add_argument("-u", "--upper_moves", help="upper bound on number of moves allowed?", type=int, default=15)
    parser.add_argument("-s", "--symbols_allowed",
                        help="symbols allowed", type=str, default='xoan<>')
    args = parser.parse_args()
    assert args.lo_symbols < args.max_symbols
    assert args.lo_moves < args.upper_moves

    try:
        main(args)
        # cur = bfs('', 3, 5, random.sample(letter_symbols, k=args.max_symbols), args.upper_moves)
        # print(cur)
    except KeyboardInterrupt as e:
        print(e)
        os.system(f'dos2unix "{TESTING_FILE_PATH}"')
        
