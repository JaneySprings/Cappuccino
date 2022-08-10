from code_generators import generate_file_content, generate_inflate, get_public_fields, get_find_views_body
from utils import get_class_name, get_file_name, get_file_paths, create_base_binding_file
from config import IMPORTS, LAYOUT_DIR_PATH, VIEW_BINDING_DIR_PATH
from xml_parser import XmlParser


def parse_xml_file(xml_file_path):
    xml_parser = XmlParser(xml_file_path)
    tag_names = xml_parser.get_tag_names()
    class_name = get_class_name(xml_file_path)

    with open(f'{VIEW_BINDING_DIR_PATH}{class_name}.cs', 'w') as file:
        file.write(generate_file_content(
            IMPORTS,
            class_name,
            get_public_fields(tag_names),
            get_find_views_body(tag_names),
            generate_inflate(class_name, get_file_name(xml_file_path, with_extension=False))
        ))


def main():
    create_base_binding_file()
    try:
        xml_file_paths = get_file_paths(LAYOUT_DIR_PATH)
    except FileNotFoundError:
        print('Layout dir not exists or path in config.py is wrong')
    else:
        for xml_file_path in xml_file_paths:
            xml_file_name = get_file_name(xml_file_path, with_extension=True)
            try:
                parse_xml_file(xml_file_path)
            except Exception as error:
                print(f'\n*----- Something went wrong with {xml_file_name} -----*')
                print(f'{error}\n')
            else:
                print(f'{xml_file_name} processed successfully!')


if __name__ == '__main__':
    print()
    main()
