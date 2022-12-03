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
  profileColor: ProfileColorGetDto;
  firstName: string;
  lastName: string;
  username: string;
  phoneNumber: string;
  email: string;
  birthday: string;
};

export type UserCreateDto = {
  profileColorId: number;
  firstName: string;
  lastName: string;
  username: string;
  password: string;
  phoneNumber: string;
  email: string;
  birthday: string;
};

export type UserUpdateDto = {
  profileColorId: number;
  firstName: string;
  lastName: string;
  username: string;
  password: string;
  phoneNumber: string;
  email: string;
};

export type GroupGetDto = {
  id: number;
  name: string;
  image: string;
};

export type GroupCreateDto = {
  name: string;
  image: string;
};

export type GroupUpdateDto = {
  name: string;
  image: string;
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
  memberRole: MemberRoleGetDto;
  user: UserGetDto;
  group: GroupGetDto;
};

export type GroupMemberCreateDto = {
  memberRoleId: number;
  userId: number;
  groupId: number;
};

export type GroupMemberUpdateDto = {
  memberRoleId: number;
  userId: number;
  groupId: number;
};

export type CalendarGetDto = {
  id: number;
  group: GroupGetDto;
};

export type CalendarCreateDto = {
  groupId: number;
};

export type CalendarUpdateDto = {
  groupId: number;
};

export type ToDoGetDto = {
  id: number;
  calendar: CalendarGetDto;
  title: string;
  description: string;
  startDate: Date;
  endDate: Date;
};

export type ToDoCreateDto = {
  calendarId: number;
  title: string;
  description: string;
  startDate: Date;
  endDate: Date;
};

export type ToDoUpdateDto = {
  calendar: CalendarGetDto;
  title: string;
  description: string;
  startDate: Date;
  endDate: Date;
};

export type EventGetDto = {
  id: number;
  calendar: CalendarGetDto;
  name: string;
  eventDetails: string;
  startDate: Date;
  endDate: Date;
};

export type EventCreateDto = {
  calendarId: number;
  name: string;
  eventDetails: string;
  startDate: Date;
  endDate: Date;
};

export type EventUpdateDto = {
  calendar: CalendarGetDto;
  name: string;
  eventDetails: string;
  startDate: Date;
  endDate: Date;
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
  mealType: MealTypeGetDto;
  calendar: CalendarGetDto;
};

export type RecipeCreateDto = {
  name: string;
  image: string;
  servings: number;
  directions: string;
  mealTypeId: number;
  calendarId: number;
};

export type RecipeUpdateDto = {
  name: string;
  image: string;
  servings: number;
  directions: string;
  mealType: MealTypeGetDto;
  calendar: CalendarGetDto;
};

export type RecipeIngredientGetDto = {
  id: number;
  recipe: RecipeGetDto;
  ingredient: IngredientGetDto;
  quantity: number;
  unit: UnitGetDto;
};

export type RecipeIngredientCreateDto = {
  recipeId: number;
  ingredientId: number;
  quantity: number;
  unitId: number;
};

export type RecipeIngredientUpdateDto = {
  recipe: RecipeGetDto;
  ingredient: IngredientGetDto;
  quantity: number;
  unit: UnitGetDto;
};

export type ShoppingListRecipeIngredientGetDto = {
  recipeIngredient: RecipeGetDto;
  shoppingList: ShoppingListGetDto;
  quantity: number;
};

export type ShoppingListRecipeIngredientCreateDto = {
  recipeIngredientId: number;
  shoppingListId: number;
  quantity: number;
};

export type ShoppingListRecipeIngredientUpdateDto = {
  recipeIngredient: RecipeGetDto;
  shoppingList: ShoppingListGetDto;
  quantity: number;
};

export type ShoppingListGetDto = {
  id: number;
  name: string;
};

export type ShoppingListCreateDto = {
  name: string;
};

export type ShoppingListUpdateDto = {
  name: string;
};

export type OptionDto = {
  text: string;
  value: number;
};
