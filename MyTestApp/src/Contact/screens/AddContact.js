import React, { Component } from "react";
import { Container } from "native-base";
import HeaderWithBack from "../components/HeaderWithBack";
import {  } from "../components/FromAdd";


export default class AddContact extends Component{
    render(){
        render(
            <Container>
                <HeaderWithBack navigation={this.props.navigation} title='Add new Contact'/>
                <FromAdd navigation={this.props.navigation} />
            </Container>
        )
    }
}