import random
import time

while True:
    with open("new.txt", "w") as file:
        x = round(random.uniform(-10, 10), 2)
        y = round(random.uniform(-10, 10), 2)
        z = round(random.uniform(-10, 10), 2)

        file.write(f"{x}\n{y}\n{z}")

    time.sleep(5)  # Wait for a second.
