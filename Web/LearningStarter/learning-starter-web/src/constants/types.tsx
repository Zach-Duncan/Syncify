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
  username: string;
  phoneNumber: string;
  email: string;
  birthDay: string;
};

export type UserCreateDto = {
  profileColorId: ProfileColorGetDto;
  firstName: string;
  lastName: string;
  username: string;
  phoneNumber: string;
  email: string;
  birthDay: string;
};

export type UserUpdateDto = {
  profileColorId: ProfileColorGetDto;
  firstName: string;
  lastName: string;
  username: string;
  phoneNumber: string;
  email: string;
};

export type GroupGetDto = {
  id: number;
  groupName: string;
  groupImage: string;
};

export type GroupCreateDto = {
  groupName: string;
  groupImage: string;
};

export type GroupUpdateDto = {
  groupName: string;
  groupImage: string;
};

export type MemberRoleGetDto = {
  id: number;
  name: string;
};

export type MemberRoleCreateDto = {
  name: string;
};

export type MemberRoleUpdateDto = {
  name: string;
};

export type GroupMemberGetDto = {
  id: number;
  memberRoleId: MemberRoleGetDto;
  userId: UserGetDto;
  groupId: GroupGetDto;
};

export type GroupMemberCreateDto = {
  memberRoleId: MemberRoleGetDto;
  userId: UserGetDto;
  groupId: GroupGetDto;
};

export type GroupMemberUpdateDto = {
  memberRoleId: MemberRoleGetDto;
  userId: UserGetDto;
  groupId: GroupGetDto;
};

export type CalendarGetDto = {
  id: number;
  groupId: GroupGetDto;
};

export type CalendarCreateDto = {
  groupId: GroupGetDto;
};

export type CalendarUpdateDto = {
  groupId: GroupGetDto;
};

export type ToDoGetDto = {
  id: number;
  calendarId: number;
  title: string;
  description: string;
  date: Date;
};

export type ToDoCreateDto = {
  calendarId: number;
  title: string;
  description: string;
  date: Date;
};

export type ToDoUpdateDto = {
  calendarId: number;
  title: string;
  description: string;
  date: Date;
};

export type EventGetDto = {
  id: number;
  calendarId: number;
  name: string;
  eventDetails: string;
  createdDate: Date;
};

export type EventCreateDto = {
  calendarId: number;
  name: string;
  eventDetails: string;
  createdDate: Date;
};

export type EventUpdateDto = {
  calendarId: number;
  name: string;
  eventDetails: string;
  createdDate: Date;
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

export type UnitGetDto = {
  id: number;
  name: string;
  abbreviation: string;
};

export type UnitCreateDto = {
  name: string;
  abbreviation: string;
};

export type UnitUpdateDto = {
  name: string;
  abbreviation: string;
};

export type RecipeGetDto = {
  id: number;
  name: string;
  image: string;
  servings: number;
  directions: string;
  mealTypeId: MealTypeGetDto;
  calendarId: CalendarGetDto;
};

export type RecipeCreateDto = {
  name: string;
  image: string;
  servings: number;
  directions: string;  
  mealTypeId: MealTypeGetDto;
  calendarId: CalendarGetDto;
};

export type RecipeUpdateDto = {
  name: string;
  image: string;
  servings: number;
  directions: string;  
  mealTypeId: MealTypeGetDto;
  calendarId: CalendarGetDto;
};

export type RecipeIngredientGetDto = {
  id: number;
  recipeId: RecipeGetDto;
  ingredientId: IngredientGetDto;
  quantity: number;
  unitId: UnitGetDto;
};

export type RecipeIngredientCreateDto = {
  recipeId: RecipeGetDto;
  ingredientId: IngredientGetDto;
  quantity: number;
  unitId: UnitGetDto;
};

export type RecipeIngredientUpdateDto = {
  recipeId: RecipeGetDto;
  ingredientId: IngredientGetDto;
  quantity: number;
  unitId: UnitGetDto;
};

export type ShoppingListRecipeIngredientGetDto = {
  recipeIngredientId: RecipeIngredientGetDto;
  shoppingListId: ShoppingListGetDto;
  quantity: number;
};

export type ShoppingListRecipeIngredientCreateDto = {
  recipeIngredientId: RecipeIngredientGetDto;
  shoppingListId: ShoppingListGetDto;
  quantity: number;
};

export type ShoppingListRecipeIngredientUpdateDto = {
  recipeIngredientId: RecipeIngredientGetDto;
  shoppingListId: ShoppingListGetDto;
  quantity: number;
};

export type ShoppingListGetDto = {
  id: number;   
  name: string; 
}; 

export type ShoppingListCreateDto = {   
  name:string; 
}; 

export type ShoppingListUpdateDto = {   
  name: string; 
};

