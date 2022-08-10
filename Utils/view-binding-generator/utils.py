from config import VIEW_BINDING_DIR_PATH
from generated_code import base_binding_content
import os


def get_file_name(path, with_extension):
    sep = '/' if path.count('/') >= 1 else '\\'
    if path.count(sep) > 0 and path.count('.') > 0:
        return path[path.rindex(sep) + 1:path.rindex('.') if not with_extension else None]
    return ''


def get_class_name(path):
    file_name = get_file_name(path, False)
    return ''.join(file_name.title().split('_')) + 'Binding'


def writeln_list(lst, tab_count):
    tab_size = 4
    for i in range(len(lst)):
        if i != 0:
            lst[i] = ' ' * tab_size * tab_count + lst[i]
    return '\n'.join(lst)


def get_file_paths(dir_path):
    return list(map(lambda file_name: f'{dir_path}{file_name}', os.listdir(dir_path)))


def create_base_binding_file():
    dst_file_path = f'{VIEW_BINDING_DIR_PATH}BaseBinding.cs'
    if not os.path.exists(dst_file_path):
        with open(dst_file_path, 'w') as file:
            file.write(base_binding_content)
