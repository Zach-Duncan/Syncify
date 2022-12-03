import axios from "axios";
import { Field, Form, Formik } from "formik";
import React, { useEffect, useState } from "react";
import { Button, Header, Input } from "semantic-ui-react";
import {
  ApiResponse,
  UnitGetDto,
  UnitUpdateDto,
} from "../../../constants/types";
import { useRouteMatch } from "react-router-dom";
import { routes } from "../../../routes/config";
import { useHistory } from "react-router-dom";
import "./unit-update.css";
import toast from "react-hot-toast";
export const UnitUpdatePage = () => {
  const history = useHistory();
  let match = useRouteMatch<{ id: string }>();
  const id = match.params.id;
  const [unit, setUnits] = useState<UnitGetDto>();

  useEffect(() => {
    const fetchUnits = async () => {
      const response = await axios.get<ApiResponse<UnitGetDto>>(
        `/api/units/${id}`
      );

      if (response.data.hasErrors) {
        console.log(response.data.errors);
        return;
      }

      setUnits(response.data.data);
    };

    fetchUnits();
  }, [id]);

  const onSubmit = async (values: UnitUpdateDto) => {
    const response = await axios.put<ApiResponse<UnitGetDto>>(
      `/api/units/${id}`,
      values
    );

    if (response.data.hasErrors) {
      response.data.errors.forEach((err) => {
        console.log(err.message);
      });
    } else {
      history.push(routes.units.listing);
      toast.success("Unit updated", {
        position: "top-center",
        style: {
          background: "#333",
          color: "#fff",
        },
      });
    }
  };
  return (
    <>
      {unit && (
        <Formik initialValues={unit} onSubmit={onSubmit}>
          <Form>
            <div className="unit-update-container">
              <Header>Update Unit</Header>
            </div>
            <div className="unit-update-container">
              <label htmlFor="name">Name</label>
            </div>
            <div className="unit-update-container">
              <Field id="name" name="name">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="unit-update-container">
              <label htmlFor="name">Abbreviation</label>
            </div>
            <div className="unit-update-container">
              <Field id="abbreviation" name="abbreviation">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="unit-update-container">
              <Button
                positive
                icon="check"
                content="Update"
                labelPosition="left"
                type="submit"
              />
            </div>
          </Form>
        </Formik>
      )}
    </>
  );
};
