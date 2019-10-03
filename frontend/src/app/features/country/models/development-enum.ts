export enum DevelopmentEnum {
    MudTractor = 0,
    Sludgeharvester,
    CoralWall,
    SonarGun,
    UnderwaterMaterialArts,
    Alchemy
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
    Object.keys(DevelopmentEnum).map(key => {
        if (DevelopmentEnum[key] === value) {
            return key;
        }
    });
}

export function getDevelopmentEnum(parameter: number | string): number | string {
    if (typeof parameter === 'number') {
        return getDevelopmentEnumName(parameter);
    }

    return getDevelopmentEnumValues(parameter);
}
