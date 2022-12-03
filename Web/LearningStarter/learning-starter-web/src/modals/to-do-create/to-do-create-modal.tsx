// import "../../modals/modal.css";
import { Field, Form, Formik } from "formik";
import React, { useEffect, useState } from "react";
import { Input, Modal, Button, Dropdown } from "semantic-ui-react";
import {
  ApiResponse,
  OptionDto,
  ToDoCreateDto,
  ToDoGetDto,
} from "../../constants/types";
import axios from "axios";
import { BaseUrl } from "../../constants/env-cars";
import toast from "react-hot-toast";

function ToDoCreateModal() {
  const [firstOpen, setFirstOpen] = useState(false);
  const [secondOpen, setSecondOpen] = useState(false);
  const [calendarOptions, setCalendarOptions] = useState<OptionDto[]>();
  console.log("debug", calendarOptions);
  const initialValues: ToDoCreateDto = {
    calendarId: 0,
    title: "",
    description: "",
    startDate: new Date(),
    endDate: new Date(),
  };

  const onSubmit = async (values: ToDoCreateDto) => {
    const response = await axios.post<ApiResponse<ToDoGetDto>>(
      `${BaseUrl}/api/to-dos`,
      values,
      {
        validateStatus: () => true,
      }
    );

    if (response.data.hasErrors) {
      response.data.errors.forEach((err) => {
        console.log(err.message);
        toast.error("Error Occured", {
          position: "top-center",
          style: {
            background: "#333",
            color: "#fff",
          },
        });
      });
    } else {
      setSecondOpen(true);
      toast.success("To-Do created", {
        position: "top-center",
        style: {
          background: "#333",
          color: "#fff",
        },
      });
    }
  };

  useEffect(() => {
    async function getCalendarOptions() {
      const response = await axios.get<ApiResponse<OptionDto[]>>(
        "/api/calendars/options"
      );

      setCalendarOptions(response.data.data);
    }

    getCalendarOptions();
  }, []);

  return (
    <>
      <Formik initialValues={initialValues} onSubmit={onSubmit}>
        <Modal
          as={Form}
          onClose={() => setFirstOpen(false)}
          onOpen={() => setFirstOpen(true)}
          open={firstOpen}
          trigger={
            <Button
              icon="circle plus"
              labelPosition="left"
              content="To-Do"
              positive
              onClick={() => setFirstOpen(true)}
            />
          }
        >
          <Modal.Header className="create-type-field">
            Create To-Do
          </Modal.Header>

          <Modal.Content>
            <Modal.Description>
              <div>
                <label htmlFor="title" className="field-title">
                  To-Do Title
                </label>
              </div>
              <Field id="title" name="title">
                {({ field }) => <Input {...field} />}
              </Field>
              <div>
                <label htmlFor="description" className="field-title">
                  To-Do Description
                </label>
              </div>
              <Field id="description" name="description">
                {({ field }) => <Input {...field} />}
              </Field>
              <div>
                <label htmlFor="startDate" className="field-title">
                  Date
                </label>
              </div>
              <Field id="startDate" name="startDate">
                {({ field }) => <Input type="date" {...field} />}
              </Field>
              <div>
                <label htmlFor="endDate" className="field-title">
                  Date
                </label>
              </div>
              <Field id="endDate" name="endDate">
                {({ field }) => <Input type="date" {...field} />}
              </Field>
              <div>
                <label htmlFor="calendarId">Calendar</label>
              </div>
              <Field name="calendarId" id="calendarId" className="field">
                {({ field, form }) => (
                  <Dropdown
                    selection
                    options={calendarOptions}
                    {...field}
                    onChange={(_, { name, value }) =>
                      form.setFieldValue(name, value)
                    }
                    onBlur={(_, { name, value }) =>
                      form.setFieldValue(name, value)
                    }
                  />
                )}
              </Field>
            </Modal.Description>
          </Modal.Content>

          <Modal.Actions className="footer">
            <Button
              type="button"
              icon="cancel"
              content="Cancel"
              labelPosition="left"
              onClick={() => setFirstOpen(false)}
              negative
            />
            <Button
              type="submit"
              icon="calendar check"
              content="Create"
              labelPosition="left"
              positive
            />
          </Modal.Actions>
          <Modal
            onClose={() => setSecondOpen(false)}
            open={secondOpen}
            size="small"
          >
            <Modal.Header>Success!</Modal.Header>
            <Modal.Content>
              <p>You have successfully created a to-do in Syncify!</p>
            </Modal.Content>
            <Modal.Actions>
              <Button
                type="button"
                icon="home"
                content="Home"
                labelPosition="right"
                positive
                onClick={() => setFirstOpen(false)}
              />
            </Modal.Actions>
          </Modal>
        </Modal>
      </Formik>
    </>
  );
}

export default ToDoCreateModal;
