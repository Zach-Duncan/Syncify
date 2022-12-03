import React from "react";
// import { Form, Formik } from "formik";
// import { TextField } from "../../components/text-field";
// import * as Yup from "yup";
// import SyncifyImg from "../../assets/Syncify.png";
// import { useHistory } from "react-router-dom";
// import { Button, Header, Modal } from "semantic-ui-react";
// import "../../App.css";
// import { UserCreateDto } from "../../constants/types";

// function RegisterModal() {
//   const [open, setOpen] = React.useState(false);
//   const initialValues: UserCreateDto={
//     profileColorId: 0,
//     firstName: "",
//     lastName: "",
//     phoneNumber: "",
//     email: "",
//     birthday: "",
//     username: "",
//     password: "",
//   }}
//   const history = useHistory();
//   validationSchema={validate}
//   onSubmit={(values) => {
//     console.log(values);
//   }}

//   return (
//     <Modal
//       onClose={() => setOpen(false)}
//       onOpen={() => setOpen(true)}
//       open={open}
//       trigger={<Button>Register</Button>}
//     >
//       <Modal.Header>Register For Membership</Modal.Header>
//       <Modal.Content image>
//         <img sizes="medium" src={SyncifyImg} />
//         <Modal.Description>
//           <Header>Sign Up</Header>
//           <Signup />
//         </Modal.Description>
//       </Modal.Content>
//       <Modal.Actions>
//         <Button color="black" onClick={() => setOpen(false)}>
//           Nope
//         </Button>
//         <Button
//           content="Yep, that's me"
//           labelPosition="right"
//           icon="checkmark"
//           onClick={() => setOpen(false)}
//           positive
//         />
//       </Modal.Actions>
//     </Modal>
//   );
// }

// export default RegisterModal;

// export const Signup = () => {
//   const validate = Yup.object({
//     profileColor: Yup.string()
//       .max(10, "Must be 15 characters or less")
//       .required("Profile is required"),
//     firstName: Yup.string()
//       .max(15, "Must be 15 characters or less")
//       .required("First Name is required"),
//     lastName: Yup.string()
//       .max(20, "Must be 20 characters or less")
//       .required("Last Name is required"),
//     phoneNumber: Yup.string()
//       .max(12, "Your phone number must in the format of 123-456-7890")
//       .min(12, "Your phone number must in the format of 123-456-7890")
//       .required("Phone number is required"),
//     email: Yup.string().email("Email is invalid").required("Email is required"),
//     birthday: Yup.string()
//       .max(10, "Your birthday must in the format of MM/DD/YYYY")
//       .min(10, "Your birthday must in the format of MM/DD/YYYY")
//       .required("Birthday is equired"),
//     username: Yup.string()
//       .min(5, "Must be 5 characters or more")
//       .required("Username is required"),
//     password: Yup.string()
//       .min(6, "Password must be at least 6 characters")
//       .required("Password is required"),
//     confirmPassword: Yup.string()
//       .oneOf([Yup.ref("password"), null], "Password must match")
//       .required("Password is required"),
//   });
//   return (
//     <Formik

//     >
//       {(formik) => (
//         <>
//           <Form>
//             <TextField label="Profile Color" name="profileColor" type="text" />
//             <TextField label="First Name" name="firstName" type="text" />
//             <TextField label="Last Name" name="lastName" type="text" />
//             <TextField label="Phone Number" name="phoneNumber" type="text" />
//             <TextField label="Email" name="email" type="email" />
//             <TextField label="Birthday" name="birthday" type="text" />
//             <TextField label="Username" name="username" type="text" />
//             <TextField label="Password" name="password" type="password" />
//             <TextField
//               label="Confirm Password"
//               name="confirmPassword"
//               type="password"
//             />
//             <button className="btn btn-dark mt-3" type="submit">
//               Register
//             </button>
//             <button className="btn btn-danger mt-3 ml-3" type="reset">
//               Reset
//             </button>
//           </Form>
//         </>
//       )}
//     </Formik>
//   );
// };
