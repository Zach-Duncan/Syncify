//This type uses a generic (<T>).  For more information on generics see: https://www.typescriptlang.org/docs/handbook/2/generics.html

//import internal from "stream";

//You probably wont need this for the scope of this class :)
export type ApiResponse<T> = {
  data: T;
  errors: Error[];
  hasErrors: boolean;
};

export type Error = {
  property: string;
  message: string;
};

export type AnyObject = {
  [index: string]: any;
};

export type ProfileColorGetDto = {
  id: number;
  colors: string;
};

export type UserGetDto = {
  id: number;
  profileColorId: ProfileColorGetDto;
  firstName: string;
  lastName: string;
  userName: string;
  phoneNumber: string;
  email: string;
  birthday: string;
};

export type UserCreateDto = {
  profileColorId: ProfileColorGetDto;
  firstName: string;
  lastName: string;
  userName: string;
  phoneNumber: string;
  email: string;
  birthday: string;
};

export type UserUpdateDto = {
  profileColorId: ProfileColorGetDto;
  firstName: string;
  lastName: string;
  userName: string;
  phoneNumber: string;
  email: string;
};

export type MealTypeGetDto = {
  id: number;
  name: string;
};

export type MealTypeCreateDto = {
  name: string;
};

export type MealTypeUpdateDto = {
  name: string;
};

export type IngredientGetDto = {
  id: number;
  name: string;
  image: string;
};

export type IngredientCreateDto = {
  name: string;
  image: string;
};

export type IngredientUpdateDto = {
  name: string;
  image: string;
};

