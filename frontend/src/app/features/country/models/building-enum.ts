export enum BuildingEnum {
    FlowController = 0,
    ReefCastle
}

function getBuildingEnumValues(name: string): number {
    const parameter = name.trim().toLowerCase();
    if (BuildingEnum.FlowController.toString().toLowerCase() === parameter) {
        return BuildingEnum.FlowController.valueOf();
    } else if (BuildingEnum.ReefCastle.toString().toLowerCase()) {
        return BuildingEnum.ReefCastle.valueOf();
    }

    return -1;
}

function getBuildingEnumName(value: number): string {
    if (value < 0 || value > 1) {
        return '';
    }
    if (value === 0) {
        return BuildingEnum.FlowController.toString();
    }

    return BuildingEnum.ReefCastle.toString();
}

export function getBuildingEnum(parameter: number | string): number | string {
    if (typeof parameter === 'number') {
        return getBuildingEnumName(parameter);
    }

    return getBuildingEnumValues(parameter);
}
