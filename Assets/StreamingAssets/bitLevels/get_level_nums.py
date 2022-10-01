import re

with open("levels.txt") as f:
    data = f.read()
with open("levels.txt", 'w') as numbered_version:
    a = data.strip().split("\n\n")
    seen = {}
    count = 0
    for line in data.splitlines():
        cur = line
        print(cur)
        if "#" in line:
            # cur = re.sub(r"(\#[^\d]+)", r"\1" + f"\{count}", line)
            left, right = line.split("#")
            right = right.rstrip("1234567890")
            # cur += str(count)
            cur = f"{left}#{right}{count}"
            count += 1
        print(cur, file=numbered_version)
    # exit()
    for i, group in enumerate(a):
        # numbered_version
        no_comments = [line if "#" not in line else line[:line.index("#")] for line in group.splitlines()]
        fixed = "\n".join(no_comments)
        if fixed in seen:
            print(fixed)
            print(i, 'was seen before at', seen[fixed])
            assert False
        seen[fixed] = i
        # print("\n", i, sep='')
        # print(fixed)
