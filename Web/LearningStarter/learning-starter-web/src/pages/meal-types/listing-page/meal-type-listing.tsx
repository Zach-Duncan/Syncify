import axios from "axios";
import React, { useEffect, useState } from "react";
import { Header, Table } from "semantic-ui-react";
import { BaseUrl } from "../../../constants/env-cars";
import { ApiResponse, MealTypeGetDto } from "../../../constants/types";
import "./meal-type-listing.css";

export const MealTypeListingPage = () => {
  const [mealTypes, setMealTypes] = useState<MealTypeGetDto[]>();

  useEffect(() => {
    const fetchMealTypes = async () => {
      const response = await axios.get<ApiResponse<MealTypeGetDto[]>>(
        `${BaseUrl}/api/meal-types`
      );

      if (response.data.hasErrors) {
        response.data.errors.forEach((err) => {
          console.log(err.message);
        });
      } else {
        setMealTypes(response.data.data);
      }
    };

    fetchMealTypes();
  }, []);

  return (
    <>
      {mealTypes && (
        <>
          <Header>Meal Types</Header>

          <Table striped celled>
            <Table.Header>
              <Table.Row>
                <Table.HeaderCell>Id</Table.HeaderCell>
                <Table.HeaderCell>Name</Table.HeaderCell>
              </Table.Row>
            </Table.Header>
            <Table.Body>
              {mealTypes.map((mealType) => {
                return (
                  <Table.Row key={mealType.id}>
                    <Table.Cell>{mealType.id}</Table.Cell>
                    <Table.Cell>{mealType.name}</Table.Cell>
                  </Table.Row>
                );
              })}
            </Table.Body>
          </Table>
        </>
      )}
    </>
  );
};
