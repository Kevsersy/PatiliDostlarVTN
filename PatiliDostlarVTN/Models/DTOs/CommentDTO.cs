namespace PatiliDostlarVTN.Models.DTOs;

public record CommentDTO(
    string? message = "No message",  
    string? avatarUrl = "No avatar", 
    string? TimeAgo = "Just now"     
);
