import { ChangeEvent,useState } from "react";
import { Button, Form, Segment } from "semantic-ui-react";
import { UserDto } from "../../models/userDto"
import apiConnector from "../../api/apiConnector";
import { useNavigate } from "react-router-dom";
import authSvc from "../../auth/authSvc";

export default function Register()
{   
    const navigate = useNavigate();
    const [userDto, setUserDto] = useState<UserDto>({
        email: '',
        password:''
    });

    async function handleLogin() {     
         authSvc.login(userDto);
         await timeout(100);
         navigate("listMovie");
    }

    function timeout(delay: number) {
        return new Promise( res => setTimeout(res, delay) );
    }   

    function handleInputChange(event: ChangeEvent<HTMLInputElement>) {
        const {name, value} = event.target;
        setUserDto({ ...userDto, [name]: value })
    }

    return (
        <>
            <Segment clearing inverted>
                <Form autoComplete='off' className="ui inverted form" >
                    <Form.Input placeholder="Email" name="email" value={userDto.email} onChange={handleInputChange} />
                    <Form.Input placeholder="Password" name="password" value={userDto.password} onChange={handleInputChange} />
                    <Button floated='right' positive type="button" content="Register" onClick={async () => apiConnector.registerUser(userDto)} ></Button>
                    <Button floated='right' positive type="button" content="Login" onClick={async () =>handleLogin()} ></Button>
                </Form>               
            </Segment>
    
    </>)
}

