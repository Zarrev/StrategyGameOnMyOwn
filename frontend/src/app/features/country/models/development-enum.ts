export enum DevelopmentEnum {
    none = -1,
    mudTractor = 0,
    sludgeharvester,
    coralWall,
    sonarGun,
    underwaterMaterialArts,
    alchemy
}

function getDevelopmentEnumValues(name: string): number {
    const parameter = name.trim().toLowerCase();
    Object.keys(DevelopmentEnum).map(key => {
        if (key.toLowerCase() === parameter) {
            return DevelopmentEnum[key];
        }
    });

    return -1;
}

function getDevelopmentEnumName(value: number): string {
    if (value < 0 || value > 5) {
        return '';
    }
    let result: string;
    Object.keys(DevelopmentEnum).map(key => {
        if (DevelopmentEnum[key] === value) {
            result = key;
        }
    });

    return result;
}

export function getDevelopmentEnum(parameter: number | string): number | string {
    if (typeof parameter === 'number') {
        return getDevelopmentEnumName(parameter);
    }

    return getDevelopmentEnumValues(parameter);
}
