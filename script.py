import os
import shutil
import sys

def categorize_files(source_folder, destination_folder):
    # Create destination folders if they don't exist
    categories = ['images', 'videos', 'documents', 'music', 'compressed', 'programs', 'others', 'folder']
    for category in categories:
        folder_path = os.path.join(destination_folder, category)
        if not os.path.exists(folder_path):
            os.makedirs(folder_path)
    
    # Get a list of files and folders in the source folder
    items = [item for item in os.listdir(source_folder) if os.path.exists(os.path.join(source_folder, item))]
    
    # Categorize folders and move them to 'folder'
    for item in items:
        item_path = os.path.join(source_folder, item)

        if os.path.isdir(item_path):
            if item.lower() not in categories:
                shutil.move(item_path, os.path.join(destination_folder, 'folder', item))
    
    # Get a list of files in the source folder
    files = [f for f in os.listdir(source_folder) if os.path.isfile(os.path.join(source_folder, f))]

    # Categorize files based on their extensions
    for file in files:
        file_extension = file.split('.')[-1].lower()

        if file_extension in ['jpg', 'jpeg', 'png', 'gif']:
            shutil.move(os.path.join(source_folder, file), os.path.join(destination_folder, 'images', file))
        elif file_extension in ['mp4', 'avi', 'mkv', 'mov']:
            shutil.move(os.path.join(source_folder, file), os.path.join(destination_folder, 'videos', file))
        elif file_extension in ['pdf', 'doc', 'docx', 'txt', 'psd', 'html', 'htm']:
            shutil.move(os.path.join(source_folder, file), os.path.join(destination_folder, 'documents', file))
        elif file_extension in ['mp3', 'wav', 'flac', 'aac']:
            shutil.move(os.path.join(source_folder, file), os.path.join(destination_folder, 'music', file))
        elif file_extension in ['zip', 'rar', '7z']:
            shutil.move(os.path.join(source_folder, file), os.path.join(destination_folder, 'compressed', file))
        elif file_extension in ['exe', 'msi']:
            shutil.move(os.path.join(source_folder, file), os.path.join(destination_folder, 'programs', file))
        else:
            shutil.move(os.path.join(source_folder, file), os.path.join(destination_folder, 'others', file))

if __name__ == "__main__":
    if len(sys.argv) != 3:
        print("Usage: python script.py <source_folder> <destination_folder>")
        sys.exit(1)

    source_folder = sys.argv[1]
    destination_folder = sys.argv[2]

    categorize_files(source_folder, destination_folder)