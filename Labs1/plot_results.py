import matplotlib.pyplot as plt
import pandas as pd
import csv
import os

# Get full path to results.csv
current_directory = os.path.dirname(os.path.abspath(__file__))
csv_file_path = os.path.join(current_directory, 'results.csv')

# Print the path for debugging purposes
print(f"Looking for results.csv at: {csv_file_path}")

# Check if the file exists
if not os.path.exists(csv_file_path):
    print(f"File not found: {csv_file_path}")
    exit(1)

data_sizes = []
elapsed_times_qpc = []
elapsed_times_tgt = []
process_times = []

with open(csv_file_path, 'r') as file:
    reader = csv.reader(file)
    for row in reader:
        data_sizes.append(int(row[0]))
        elapsed_times_qpc.append(float(row[1]))
        elapsed_times_tgt.append(float(row[2]))
        process_times.append(float(row[3]))

# Create a DataFrame for the table
data = {
    'Data Size (KB)': data_sizes,
    'Elapsed Time (QueryPerformanceCounter) (s)': elapsed_times_qpc,
    'Elapsed Time (timeGetTime) (s)': elapsed_times_tgt,
    'Process Time (s)': process_times
}
df = pd.DataFrame(data)

# Plot the graph
plt.figure(figsize=(10, 6))

plt.plot(data_sizes, elapsed_times_qpc, label='Elapsed Time (QueryPerformanceCounter)')
plt.plot(data_sizes, elapsed_times_tgt, label='Elapsed Time (timeGetTime)')
plt.plot(data_sizes, process_times, label='Process Time')

plt.xlabel('Data Size (KB)')
plt.ylabel('Time (seconds)')
plt.title('Performance Measurement')
plt.legend()
plt.grid(True)
plt.show()

# Display the table
print(df)

