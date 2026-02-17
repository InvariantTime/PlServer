
export type ObjectType = {
    fullName: string,
    class: ObjectTypeClass
}

export enum ObjectTypeClass {
    Number,
    String,
    Enum,
    Object
}

export const CreateObjectType = (cl: ObjectTypeClass): ObjectType => {

    if (cl === ObjectTypeClass.Number)
        return {fullName: "number", class: ObjectTypeClass.Number};

    if (cl === ObjectTypeClass.String)
        return {fullName: "string", class: ObjectTypeClass.String};

    if (cl === ObjectTypeClass.Enum)
        return {fullName: "enum", class: ObjectTypeClass.Enum};

    return {fullName: "object", class: ObjectTypeClass.Object};
}