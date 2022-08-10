from utils import writeln_list


# ----------------------- clear generators -----------------------

def generate_import(lib):
    return f'using {lib};'


def generate_public_field(tag_class_name, tag_instance_name):
    return f'public {tag_class_name} {tag_instance_name} = null!;'


def generate_bind(tag_class_name, tag_instance_name):
    return f'this.{tag_instance_name} = {tag_class_name}.Bind(view);'


def generate_find_view_by_id(tag_instance_name, tag_class_name):
    return f'this.{tag_instance_name} = view.FindViewById<{tag_class_name}>(Resource.Id.{tag_instance_name})!;'


def generate_inflate(class_name, xml_file_name):
    return f"""public static {class_name} Inflate(LayoutInflater inflater, ViewGroup? parent = null, bool attachToRoot = false) {{
            return new {class_name}(inflater.Inflate(Resource.Layout.{xml_file_name}, parent, attachToRoot)!);
        }}"""


def generate_file_content(imports, class_name, public_fields, find_views_body, inflate):
    return f"""{writeln_list(imports, 0)}

namespace Cappuccino.App.Android.ViewBinding {{

    public class {class_name}: BaseBinding {{
        {writeln_list(public_fields, 2)}

        protected {class_name}(View view) : base(view) {{ }}

        protected override void FindViews(View view) {{
            {writeln_list(find_views_body, 3)}
        }}

        {inflate}

        public static {class_name} Bind(View view) {{
            return new {class_name}(view);
        }}
    }}
}}
"""


# ----------------------- generator helpers -----------------------

def get_public_fields(tag_names):
    return list(map(lambda tag_name: generate_public_field(tag_name['class_name'], tag_name['instance_name']), tag_names))


def get_find_views_body(tag_names):
    find_views_body = []
    for tag_name in tag_names:
        tag_class_name = tag_name['class_name']
        tag_instance_name = tag_name['instance_name']
        if tag_class_name.find('Binding') != -1:
            find_views_body.append(generate_bind(tag_class_name, tag_instance_name))
        else:
            find_views_body.append(generate_find_view_by_id(tag_instance_name, tag_class_name))
    return find_views_body

