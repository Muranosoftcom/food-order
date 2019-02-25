import { DayOfWeek } from "../day-of-week";

export interface UserDto {
	id: string;
	name: string;
	password: string;
}

export interface DayMenuDto {
	shortDate: string;
	suppliers: [];
}

export interface DishDto {
	id: string;
	name: string;
	price: number;
	negativeReviews: number;
	positiveReviews: number;
	categoryId: string;
	availableAt: DayOfWeek[];
}

export interface DishCategoryDto {
	id: string;
	name: string;
	canMultiSelect: boolean;
	position: number;
	dishes: DishDto[];
}

export interface SupplierDto {
	supplierId: string;
	supplierName: string;
	availableMoneyToOrder: number;
	canMultiSelect: boolean;
	categories: DishCategoryDto[];
}
