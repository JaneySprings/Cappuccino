import xml.etree.cElementTree as ET


class XmlParser:
    def __init__(self, xml_file_path):
        self.__tree = ET.ElementTree(file=xml_file_path)
        self.__namespaces = {'android': 'http://schemas.android.com/apk/res/android'}

    def __get_elements_with_id(self):
        return self.__tree.findall('.//*[@android:id]', self.__namespaces)

    def __get_attr_value(self, element, attr, namespace="android"):
        formatted_attr = f'{{{self.__namespaces[namespace]}}}{attr}'
        return element.attrib[formatted_attr]

    def __get_class_name(self, element):
        id_value = self.__get_attr_value(element, 'id')
        tag = element.tag
        if tag == 'include':
            return id_value[id_value.index('/') + 1:].title() + 'Binding'
        if tag.count('.') > 0:
            return tag[tag.rindex('.') + 1:]
        return tag

    def __get_instance_name(self, element):
        id_value = self.__get_attr_value(element, 'id')
        return id_value[id_value.index('/') + 1:]

    def get_tag_names(self):
        names = []
        for element in self.__get_elements_with_id():
            if element.tag != 'fragment':
                names.append({
                    'class_name': self.__get_class_name(element),
                    'instance_name': self.__get_instance_name(element)
                })
        return names
