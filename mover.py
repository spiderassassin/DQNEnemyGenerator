import os
import shutil
import argparse

def collect_and_rename(input_dir, output_dir, k):
    # Track the current index for naming files
    current_index = k

    # Get all trial_x folders (ignore anything that’s not a dir)
    trial_dirs = [d for d in os.listdir(input_dir)
                  if os.path.isdir(os.path.join(input_dir, d)) and d.startswith("trial_")]

    # Ensure output subdirectories exist
    for subfolder in ['0', '5', '10', '15']:
        os.makedirs(os.path.join(output_dir, subfolder), exist_ok=True)

    for trial in trial_dirs:
        trial_path = os.path.join(input_dir, trial)

        # Paths to subdirectories 5, 10, and 15
        path_5 = os.path.join(trial_path, '5')
        path_10 = os.path.join(trial_path, '10')
        path_15 = os.path.join(trial_path, '15')

        # 0: copy trial_x/5/initial.csv → output_dir/0/<k>.csv
        initial_src = os.path.join(path_5, 'initial.csv')
        if os.path.exists(initial_src):
            shutil.copy(initial_src, os.path.join(output_dir, '0', f'{current_index}.csv'))

        # 5: copy trial_x/5/final.csv → output_dir/5/<k>.csv
        final_5_src = os.path.join(path_5, 'final.csv')
        if os.path.exists(final_5_src):
            shutil.copy(final_5_src, os.path.join(output_dir, '5', f'{current_index}.csv'))

        # 10: copy trial_x/10/final.csv → output_dir/10/<k>.csv
        final_10_src = os.path.join(path_10, 'final.csv')
        if os.path.exists(final_10_src):
            shutil.copy(final_10_src, os.path.join(output_dir, '10', f'{current_index}.csv'))

        # 15: copy trial_x/15/final.csv → output_dir/15/<k>.csv
        final_15_src = os.path.join(path_15, 'final.csv')
        if os.path.exists(final_15_src):
            shutil.copy(final_15_src, os.path.join(output_dir, '15', f'{current_index}.csv'))

        current_index += 1

if __name__ == '__main__':
    parser = argparse.ArgumentParser(description='Collect and rename experiment result files.')
    parser.add_argument('input_dir', help='Input directory containing trial_x folders')
    parser.add_argument('output_dir', help='Output directory containing folders 0, 5, 10, 15')
    parser.add_argument('k', type=int, help='Starting index for output file names')

    args = parser.parse_args()
    collect_and_rename(args.input_dir, args.output_dir, args.k)
